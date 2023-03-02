using UnityEngine;


/*
 * Detects collisions on trigger colliders & rigid bodies.
 * TODO: is this how we want to collision-detect ?
 */
public abstract class CollidableArea : MonoBehaviour
{
    /*
     * This fn gets overriden by the child to implement whatever action
     * is appropriate on collision. Whatever method we use for collision detection,
     * it should call the handler.
     * 
     * TODO: Some items (e.g. moving enemies) may need different collision handlers
     * depending on what is collided with, e.g. moving enemies colliding with terrain
     * vs. players. Not sure if this is info held in Collider2D.
     */
    protected abstract void collisionHandler(Collider2D other);

    /*
     * This fn does the actual collision detection.
     * Currently, child classes rely on this being triggered 1x at enter and not continuously,
     * so any implementation change will have to take that into account.
     */
    private void OnTriggerEnter2D(Collider2D other)
    { 
        collisionHandler(other);
    }

}
