<script lang="ts">
  import { Link } from "svelte-routing";
    import apiClient from "../../communication/api/api-client";

  let name: string = "";
  let newListId: string;
</script>

<div class="center">
  <input bind:value={name} placeholder="Name der Liste" />
  <div>
    <button
      on:click={async () =>
        (newListId = await apiClient.createShoppingListAsync(name))}
      >Erstellen</button
    >
  </div>

  {#if !!newListId}
    Eine neue Liste wurde für dich erstellt. Merke dir den Link zu dieser Liste!
    <Link to={`/shopping-list/${newListId}`}>Zur Liste</Link>
  {/if}
</div>

<style>
  .center {
    text-align: center;
  }
</style>
