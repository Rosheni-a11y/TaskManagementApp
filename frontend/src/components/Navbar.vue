<template>
    <div v-if="!isInWebView" class="w-full py-3 px-4 flex items-center gap-3" style="background-color: #41431B;">
        <svg width="40" height="40" viewBox="0 0 80 80" xmlns="http://www.w3.org/2000/svg">
            <circle cx="40" cy="40" r="40" fill="#E3DBBB" />
            <circle cx="40" cy="40" r="30" fill="#AEB784" />
            <path d="M 25,40 L 36,51 L 58,25" stroke="#41431B" stroke-width="7" stroke-linecap="round" stroke-linejoin="round" fill="none" />
        </svg>
        <h1 class="text-xl font-bold" style="color: #F8F3E1;">NexTick</h1>
    </div>
</template>

<script lang="ts">
export default {
    name: 'Navbar',
    data() {
        return {
            isInWebView: /wv/.test(navigator.userAgent)
        };
    }
};
</script>