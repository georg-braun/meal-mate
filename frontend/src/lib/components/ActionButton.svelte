<script>
    export let action;
    export let background;
    export let hoverBackground;
</script>

<button on:click={action} class="border boder-black px-2 text-lg bg-slate-700 mt-2 text-white text-center">
    <slot />
</button>
