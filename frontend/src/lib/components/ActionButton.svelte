<script>
    export let action;
    export let background;
    export let hoverBackground;
</script>

<button on:click={action} class="{background} {hoverBackground} p-2 border ">
    <slot />
</button>
