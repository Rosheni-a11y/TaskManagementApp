<template>
    <transition name="toast-slide">
        <div v-if="store.toast.visible"
            class="fixed top-4 right-4 z-50 px-4 py-3 rounded-lg shadow-lg text-sm font-medium max-w-xs border"
            :style="store.toast.type === 'success'
                ? 'background-color: #E3DBBB; color: #41431B; border-color: #AEB784;'
                : 'background-color: #f5d5d0; color: #7a2e22; border-color: #d99a8f;'">
            {{ store.toast.message }}
        </div>
    </transition>
</template>

<script lang="ts">
import { store } from '../store';

export default {
    name: 'Toast',
    data() {
        return { store };
    }
};
</script>

<style scoped>
.toast-slide-enter-active,
.toast-slide-leave-active {
    transition: all 0.3s ease;
}
.toast-slide-enter-from,
.toast-slide-leave-to {
    opacity: 0;
    transform: translateX(100%);
}
</style>