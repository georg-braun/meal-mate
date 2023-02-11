<script lang="ts">
  import { onMount } from "svelte";
  import apiClient from "../communication/api-client";
  import type { GetCategoriesDetailsQueryDto } from "../communication/dtos/GetCategoriesDetailsQuery";
  import Category from "./Category.svelte";
    

    let newCategoryName: string;

    let categories : GetCategoriesDetailsQueryDto[] = []
    onMount(async () => {
        categories = await apiClient.getCategoriesDetailsAsync();
        console.log(categories)
    })
    
</script>

<h1>Kategorie</h1>
{#each categories as category}  
    <Category category={category} />
{/each}

<div>
    Kategorie
    <input bind:value={newCategoryName} />
    <button
        on:click={async () =>
            await apiClient.createCategoryAsync(newCategoryName)}
        >Hinzufügen</button
    >
</div>
