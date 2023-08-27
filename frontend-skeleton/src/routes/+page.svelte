<!-- YOU CAN DELETE EVERYTHING IN THIS PAGE -->

<div class="container h-full mx-auto flex justify-center items-center">
	<div class="space-y-10 text-center flex flex-col items-center">
		<h3 class="h3">Schnelles Einakufszettel-Schreiben</h3>
		<p>Vorlagen helfen dabei den Einkaufszettel zu füllen und sparen dir Zeit</p>
		<h3 class="h3">Überblick über deinen Einkauf</h3>
		<p>Du hast immer im Blick was du bei deinem nächsten Einkauf besogen musst.</p>
		<h3 class="h3">Live Aktualisierung</h3>
		<p>Hake Gegenstände von deiner Liste während des Einkaufs ab und schicke Live-Aktualisierungen an Interessierte.</p>
	</div>
</div>

<style lang="postcss">
	figure {
		@apply flex relative flex-col;
	}
	figure svg,
	.img-bg {
		@apply w-64 h-64 md:w-80 md:h-80;
	}
	.img-bg {
		@apply absolute z-[-1] rounded-full blur-[50px] transition-all;
		animation: pulse 5s cubic-bezier(0, 0, 0, 0.5) infinite,
			glow 5s linear infinite;
	}
	@keyframes glow {
		0% {
			@apply bg-primary-400/50;
		}
		33% {
			@apply bg-secondary-400/50;
		}
		66% {
			@apply bg-tertiary-400/50;
		}
		100% {
			@apply bg-primary-400/50;
		}
	}
	@keyframes pulse {
		50% {
			transform: scale(1.5);
		}
	}
</style>
