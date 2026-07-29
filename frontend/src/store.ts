import { reactive } from 'vue';

export const store = reactive({
    authToken: '',
    selectedUserId: '',
    toast: {
        message: '',
        visible: false,
        type: 'success' as 'success' | 'error'
    }
});

let toastTimeout: ReturnType<typeof setTimeout> | null = null;

export function showToast(message: string, type: 'success' | 'error' = 'success') {
    store.toast.message = message;
    store.toast.type = type;
    store.toast.visible = true;

    if (toastTimeout) clearTimeout(toastTimeout);
    toastTimeout = setTimeout(() => {
        store.toast.visible = false;
    }, 3000);
}