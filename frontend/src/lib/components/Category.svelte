<script lang="ts">
  import { onMount } from "svelte";
  import apiClient from "../communication/api-client";
  import type { GetCategoriesDetailsQueryDto } from "../communication/dtos/GetCategoriesDetailsQuery";
  import Item from "./Item.svelte";
    

  export let category : GetCategoriesDetailsQueryDto;
    let newItemName: string = "";
    
</script>

    <div title={category.id}>{category.name}</div>
    {#each category.items as item (item.id)}
        <Item item={item} />
    {/each}
    


<div>
    Gegenstand
    <input bind:value={newItemName} />
    <button
        on:click={async () =>
            await apiClient.createItemAsync(category.id, newItemName)}
        >Hinzufügen</button
    >
</div>
