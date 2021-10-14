using Godot;

namespace Quoridor.Game
{
    public class SceneLoader : Node
    {
        public Control CurrentScene { get; set; }

        public override void _Ready()
        {
            
        }
        
        public void GotoScene(Node scene)
        {
            // This function will usually be called from a signal callback,
            // or some other function from the current scene.
            // Deleting the current scene at this point is
            // a bad idea, because it may still be executing code.
            // This will result in a crash or unexpected behavior.

            // The solution is to defer the load to a later time, when
            // we can be sure that no code from the current scene is running:

            CallDeferred(nameof(DeferredGotoScene), scene);
        }

        public void DeferredGotoScene(Control scene)
        {
            CurrentScene?.Hide();
            CurrentScene = scene;
            CurrentScene.Show();
            
            // Optionally, to make it compatible with the SceneTree.change_scene() API.
            GetTree().CurrentScene = CurrentScene;
        }
    }
    
    
}