<script lang="ts">
  import App from "../../App.svelte";
  import apiClient from "../communication/api-client";
  import type { ShoppingListQueryResponse } from "../communication/dtos/ShoppingListQueryResponse";
  import ShoppingListEntry from "./ShoppingListEntry.svelte";

  $: {
    refreshShoppingList(id);
  }

  let shoppingList: ShoppingListQueryResponse;

  

  async function refreshShoppingList(id: string) {
    shoppingList = await apiClient.getShoppingListAsync(id);
  }

  export let id: string;
</script>

{#if !!shoppingList}
  <h1>Einkaufsliste</h1>
  <h2>{shoppingList.name}</h2>

  {#each shoppingList.entries as entry (entry.id)}
    <ShoppingListEntry {entry} />
  {/each}
{:else}
  Lade Liste ({id}) ...
{/if}
