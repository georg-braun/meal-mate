<script lang="ts">
  import apiClient from "../../communication/api/api-client";
  import type { ItemsQueryResponse } from "../../communication/api/queries/ItemsQueryResponse";

  export let item: ItemsQueryResponse;
</script>

<div style="margin-left: 10px;" title={item.id}>
  {item.name}
  {item.categoryName}
  <button on:click={async () => await apiClient.deleteItemAsync(item.id)}
    >Löschen</button
  >
</div>
