<script lang="ts">

  import type { ShoppingListQueryResponseEntry } from "../communication/dtos/ShoppingListQueryResponse";

  export let entry: ShoppingListQueryResponseEntry;
</script>

{entry.qualifier}
{entry.itemName}