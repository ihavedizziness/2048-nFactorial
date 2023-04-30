using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TileDirection
{
    Up,
    Down,
    Left,
    Right
}

public class TileBot : MonoBehaviour
{
    public int SecondDelay;

    public GameManager Manager;
    public TileDirection Direction;

    public bool IsMoving;

    private void Update()
    {
        if (!Manager.IsBotControlling)
            return;


        if (IsMoving)
            return;

        var randomDirection = Random.Range(0, 4);
        Direction = (TileDirection)randomDirection;


        StartCoroutine(MoveDelay(Direction, SecondDelay));
    }

    public IEnumerator MoveDelay(TileDirection direction, int delay)
    {
        IsMoving = true;

        yield return new WaitForSeconds(delay);

        switch (direction)
        {
            case TileDirection.Up:
                Manager.board.Move(Vector2Int.up, 0, 1, 1, 1);
                break;
            case TileDirection.Down:
                Manager.board.Move(Vector2Int.down, 0, 1, Manager.board.Grid.height - 2, -1);
                break;
            case TileDirection.Left:
                Manager.board.Move(Vector2Int.left, 1, 1, 0, 1);
                break;
            case TileDirection.Right:
                Manager.board.Move(Vector2Int.right, Manager.board.Grid.width - 2, -1, 0, 1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        IsMoving = false;
    }
}