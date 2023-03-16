<script lang="ts">
    import { onMount } from "svelte";
    import apiClient from "../../communication/api-client";
    import { itemsStore } from "../../store";
    import Item from "./Item.svelte";

    onMount(async () => {
        await apiClient.refreshItemsStoreAsync();
    });
</script>

{#each $itemsStore as item (item.id)}
    <Item {item} />
{/each}
