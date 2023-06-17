<script lang="ts">
  import apiClient from "../../communication/api/api-client";
  import type { ItemsQueryResponse } from "../../communication/api/queries/ItemsQueryResponse";
  import ActionButton from "../ActionButton.svelte";

  export let item: ItemsQueryResponse;
</script>

<div class="grid grid-cols-3 my-2" title={item.id}>
  <div>
    {item.categoryName ?? "Unbekannte Kategorie"}
  </div>
  <div>
    {item.name}
  </div>
  <div>
    <ActionButton
      background="bg-red-300"
      action={async () => await apiClient.deleteItemAsync(item.id)}
      >Löschen</ActionButton
    >
  </div>
</div>
