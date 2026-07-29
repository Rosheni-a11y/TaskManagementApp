<template>
    <div class="min-h-screen w-full flex items-center justify-center px-6 app-gradient-bg">
        <LoginForm @login-success="handleLoginSuccess" />
    </div>
</template>

<script lang="ts">
import LoginForm from '../components/LoginForm.vue';
import { store } from '../store';

export default {
    name: 'LoginView',
    components: { LoginForm },
    methods: {
        handleLoginSuccess(token: string) {
            store.authToken = token;
            store.selectedUserId = '';
            this.$router.push('/dashboard');
        }
    }
};
</script>