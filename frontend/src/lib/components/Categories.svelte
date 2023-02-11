<script lang="ts">
  import { onMount } from "svelte";
  import apiClient from "../communication/api-client";
  import type { GetCategoriesDetailsQueryDto } from "../communication/dtos/GetCategoriesDetailsQuery";
    

    let newCategoryName: string;

    let categories : GetCategoriesDetailsQueryDto[] = []
    onMount(async () => {
        categories = await apiClient.getCategoriesDetailsAsync();
        console.log(categories)
    })
    
</script>

<h1>Kategorie</h1>
{#each categories as category}
    <div title={category.id}>{category.name}</div>
    {#each category.items as item}
        <div style="margin-left: 10px;" title={item.id}>{item.name}</div>
    {/each}
{/each}

<div>
    <input bind:value={newCategoryName} />
    <button
        on:click={async () =>
            await apiClient.createCategoryAsync(newCategoryName)}
        >Hinzufügen</button
    >
</div>
