<template>
    <router-view />
    <Toast />
</template>

<script lang="ts">
import Toast from './components/Toast.vue';

export default {
    name: 'App',
    components: { Toast }
};
</script>