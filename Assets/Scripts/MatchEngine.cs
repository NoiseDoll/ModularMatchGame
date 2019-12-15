using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchEngine : MonoBehaviour
{
    public MatchAssetManager assetManager;
    public InputPanel inputPanel;
    public Camera cam;

    private Board board;

    private LevelLoaderSystem loader;
    private SpawnSystem spawner;
    private MoveItemsSystem mover;
    private VisualizerSystem visualizer;
    private ClickSystem remover;


    void Awake()
    {
        board = new Board();
        loader = new LevelLoaderSystem(assetManager);
        spawner = new SpawnSystem();
        mover = new MoveItemsSystem();
        remover = new ClickSystem();

        visualizer = new VisualizerSystem();

        loader.Execute(board);
        MainLoop();
    }

    private void OnEnable()
    {
        inputPanel.Clicked += InputPanel_Clicked;
    }

    private void OnDisable()
    {
        inputPanel.Clicked -= InputPanel_Clicked;
    }

    private void InputPanel_Clicked(UnityEngine.EventSystems.PointerEventData obj)
    {
        var worldPoint = cam.ScreenToWorldPoint(obj.position);
        remover.Execute(board, worldPoint);
        MainLoop();

    }

    private void MainLoop()
    {
        bool isSpawned;
        bool isMoved;
        do
        {
            isSpawned = spawner.Execute(board);
            isMoved = mover.Execute(board);
        } while (isSpawned || isMoved);

        visualizer.Execute(board);
    }
}
