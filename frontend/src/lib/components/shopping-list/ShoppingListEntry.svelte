<script lang="ts">
  import apiClient from "../../communication/api-client";

  import type { ShoppingListQueryResponseEntry } from "../../communication/ShoppingListQueryResponse";

  export let shoppingListId: string;
  export let entry: ShoppingListQueryResponseEntry;
</script>

<button class="item" placeholder={entry.id} on:click={async () =>
  await apiClient.deleteEntryAsync(shoppingListId, entry.id)}>
  <div class="item__name">
    {entry.itemName}
  </div>
  <div class="item__qualifier">
    {entry.qualifier}
  </div>

<!--   <button
    Check</button
  > -->
    </button>

<style>
  .item {
    border: none;
    height: 100px;
    width: 100px;
    background-color: firebrick;
    text-align: center;
    color: white;
  }

  .item__name {
    font-weight: bold;
  }

  .item__qualifier {
  }
</style>
