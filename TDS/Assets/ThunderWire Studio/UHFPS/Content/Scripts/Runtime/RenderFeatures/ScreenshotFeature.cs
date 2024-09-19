using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UHFPS.Runtime.Rendering
{
    public class ScreenshotFeature : ScriptableRendererFeature
    {
        public RenderPassEvent RenderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public Vector2Int OutputImageSize = new(640, 360);

        public static ScreenshotFeature Instance { get; private set; }
        public ScreenshotPass Pass => scriptablePass;

        private ScreenshotPass scriptablePass;

        public override void Create()
        {
            if (Instance != null) 
                return;

            scriptablePass = new ScreenshotPass(RenderPassEvent, OutputImageSize);
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (scriptablePass == null) return;
            renderer.EnqueuePass(scriptablePass);
        }

        public class ScreenshotPass : ScriptableRenderPass
        {
            private RenderTexture _destination;
            private Vector2Int renderTextureSize;

            public ScreenshotPass(RenderPassEvent renderPassEvent, Vector2Int imageSize)
            {
                this.renderPassEvent = renderPassEvent;
                renderTextureSize = imageSize;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                CommandBuffer cmd = CommandBufferPool.Get("Screenshot");

                _destination = RenderTexture.GetTemporary(renderTextureSize.x, renderTextureSize.y, 24, RenderTextureFormat.ARGB32);
                _destination.name = "Screenshot";

#if UNITY_2022_1_OR_NEWER
                var sourceRT = renderingData.cameraData.renderer.cameraColorTargetHandle.rt;
#else
				var sourceRT = renderingData.cameraData.renderer.cameraColorTarget;
#endif

                cmd.Blit(sourceRT, _destination);
                context.ExecuteCommandBuffer(cmd);

                RenderTexture.ReleaseTemporary(_destination);
                CommandBufferPool.Release(cmd);
            }

            public Texture2D CaptureScreen()
            {
                RenderTexture.active = _destination;
                Texture2D texture2D = new Texture2D(_destination.width, _destination.height, TextureFormat.RGBA32, false);
                texture2D.ReadPixels(new Rect(0, 0, _destination.width, _destination.height), 0, 0);
                texture2D.Apply();

                RenderTexture.active = null;
                return texture2D;
            }

            public IEnumerator CaptureScreen(string outputPath)
            {
                RenderTexture.active = _destination;
                Texture2D texture2D = new(_destination.width, _destination.height, TextureFormat.RGBA32, false);
                texture2D.ReadPixels(new Rect(0, 0, _destination.width, _destination.height), 0, 0);
                texture2D.Apply();

                RenderTexture.active = null;
                byte[] bytes = texture2D.EncodeToPNG();
                File.WriteAllBytes(outputPath, bytes);
                yield return null;
            }
        }
    }
}