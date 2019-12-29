using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchEngine : MonoBehaviour
{
    public MatchAssetManager assetManager;
    public InputPanel inputPanel;
    public Camera cam;

    [Header("Systems")]
    public InputSystem input;

    private Board board;

    private LevelLoaderSystem loader;
    private SpawnSystem spawner;
    private MoveItemsSystem mover;
    private VisualizerSystem visualizer;

    void Awake()
    {
        board = new Board();
        loader = new LevelLoaderSystem(assetManager);
        spawner = new SpawnSystem();
        mover = new MoveItemsSystem();

        StartCoroutine(StartGameDelayed());
    }

    private IEnumerator StartGameDelayed()
    {
        yield return new WaitForSeconds(0.5f);

        loader.Execute(board);
        visualizer = new VisualizerSystem(board);

        MainLoop();
    }

    private void InputPanel_Clicked(UnityEngine.EventSystems.PointerEventData obj)
    {
        var worldPoint = cam.ScreenToWorldPoint(obj.position);
        input.Execute(board, worldPoint);
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

    private void OnEnable()
    {
        inputPanel.Clicked += InputPanel_Clicked;
    }

    private void OnDisable()
    {
        inputPanel.Clicked -= InputPanel_Clicked;
    }
}
