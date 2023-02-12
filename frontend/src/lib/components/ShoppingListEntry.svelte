<script lang="ts">
    import apiClient from "../communication/api-client";


  import type { ShoppingListQueryResponseEntry } from "../communication/dtos/ShoppingListQueryResponse";

  export let shoppingListId: string;
  export let entry: ShoppingListQueryResponseEntry;
</script>

{entry.qualifier}
{entry.itemName}
<button on:click={async () => await apiClient.deleteEntryAsync(shoppingListId, entry.entryId)}>Check</button>