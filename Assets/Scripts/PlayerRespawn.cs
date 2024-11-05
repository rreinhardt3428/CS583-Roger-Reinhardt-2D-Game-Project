using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float respawnDelay = 3f;  // Time in seconds to wait before respawning
    public float invulnerability = 2f;

    private SpriteRenderer spriteRenderer;
    private MeshRenderer meshRenderer;
    private CircleCollider2D playerCollider;
    private Vector3 spawnPosition;    // The position to respawn the player
    private PlayerShooting playerShooting;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        playerCollider = GetComponent<CircleCollider2D>();
        spawnPosition = transform.position;
        playerShooting = player.GetComponent<PlayerShooting>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Respawn()
    {
        spriteRenderer.enabled = false;
        meshRenderer.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        playerShooting.shootingEnabled = false;

        yield return new WaitForSeconds(respawnDelay);

        transform.position = spawnPosition;

        spriteRenderer.enabled = true;
        meshRenderer.enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        playerShooting.shootingEnabled = true;
        StartCoroutine(InvulnerabilityPeriod());
    }
    private IEnumerator InvulnerabilityPeriod()
    {
        playerCollider.enabled = false; // Disable collider to prevent damage

        Color originalColor = spriteRenderer.color;
        Color invulnerableColor = originalColor;
        invulnerableColor.a = 0.5f;  // Set opacity to 50%
        spriteRenderer.color = invulnerableColor;

        yield return new WaitForSeconds(invulnerability);

        spriteRenderer.color = originalColor;
        playerCollider.enabled = true;
    }
}
