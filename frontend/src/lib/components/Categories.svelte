<script lang="ts">
  import apiClient from "../communication/api-client";
    

    let newCategoryName: string;
    
</script>

<h1>Kategorie</h1>
<!-- {#each $categoryStore as category}
    <div>{category.title}</div>
{/each} -->

<div>
    <input bind:value={newCategoryName} />
    <button
        on:click={async () =>
            await apiClient.createCategoryAsync(newCategoryName)}
        >Hinzufügen</button
    >
</div>
