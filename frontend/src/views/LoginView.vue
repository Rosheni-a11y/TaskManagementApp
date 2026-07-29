<template>
    <div class="min-h-screen w-full app-gradient-bg">
        <Navbar />
        <div class="flex items-center justify-center px-6" style="min-height: calc(100vh - 68px);">
            <LoginForm @login-success="handleLoginSuccess" />
        </div>
    </div>
</template>

<script lang="ts">
import Navbar from '../components/Navbar.vue';
import LoginForm from '../components/LoginForm.vue';
import { store } from '../store';

export default {
    name: 'LoginView',
    components: { Navbar, LoginForm },
    methods: {
        handleLoginSuccess(token: string) {
            store.authToken = token;
            store.selectedUserId = '';
            this.$router.push('/dashboard');
        }
    }
};
</script>