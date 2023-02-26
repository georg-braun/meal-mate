<script lang="ts">
  import apiClient from "../communication/api-client";

  import type { ShoppingListQueryResponseEntry } from "../communication/dtos/ShoppingListQueryResponse";

  export let shoppingListId: string;
  export let entry: ShoppingListQueryResponseEntry;
</script>

<div placeholder={entry.id}>
  {entry.qualifier}
  {entry.itemName}

  <button
    on:click={async () =>
      await apiClient.deleteEntryAsync(shoppingListId, entry.id)}>Check</button
  >
</div>
