<script lang="ts">
  import apiClient from "../communication/api-client";



export let item : {name: string, id: string};

</script>

<div style="margin-left: 10px;" title={item.id}>{item.name}
<button on:click={async () => await apiClient.deleteItemAsync(item.id)}>Löschen</button>
</div>