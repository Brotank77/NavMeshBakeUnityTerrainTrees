# NavMeshBakeUnityTerrainTrees
Unity Terrain Trees do NOT bake into the NavMesh by default — which means AI can walk straight through forests like they don’t exist. Super frustrating… until now.
Once you add the script to your project, you will be able to navigate to the tools bar on the top of your project, and select "Bake Trees with temporary capsules"
a new window will pop up (Tree obstacle baker)
Click on the button "Generate capsules for trees"
upon doing so, you will now have a new parent object TreeObstacle. You will capsules everywhere you have a unity terrain tree. You can now use the Navmesh Surface in your screen and hit bake. This will bake the navmesh around the capsules. Since this isn't carving the nav mesh , you can actually delete the parent object and all its children and you are done!.
