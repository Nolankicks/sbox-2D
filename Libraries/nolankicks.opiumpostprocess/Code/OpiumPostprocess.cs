using System;
using Sandbox;
using Sandbox.Utility;
[Icon("waves"), Category("Post Processing")]
public sealed class OpiumPostprocess : PostProcess, Component.ExecuteInEditor
{
	IDisposable renderHook;

	protected override void OnEnabled()
	{
		renderHook = Camera.AddHookBeforeOverlay( "My Post Processing", 1000, RenderEffect);
	}
	protected override void OnDisabled()
	{
		renderHook?.Dispose();
		renderHook = null;
	}
	RenderAttributes attributes = new RenderAttributes();
	public void RenderEffect(SceneCamera camera)
	{
		if (!camera.EnablePostProcessing) return;
		Graphics.GrabFrameTexture("ColorBuffer", attributes);
		Graphics.Blit(Material.FromShader(Cloud.Shader("facepunch.goback_postprocess")), attributes);
	}
}
