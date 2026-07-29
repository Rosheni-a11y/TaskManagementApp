<template>
    <div class="w-full max-w-sm mx-auto p-6 rounded-2xl glass-card">
        <h2 class="text-xl font-semibold mb-4 text-brand text-center">Welcome</h2>
        <form @submit.prevent="handleLogin" class="space-y-4">
            <div>
                <label for="email" class="block text-sm font-medium mb-1 text-brand">Email</label>
                <input id="email" type="email" v-model="email" required
                    class="w-full px-3 py-2 rounded-lg glass-input" />
            </div>
            <div>
                <label for="password" class="block text-sm font-medium mb-1 text-brand">Password</label>
                <div class="relative">
                    <input :type="showPassword ? 'text' : 'password'" id="password" v-model="password" required
                        class="w-full px-3 py-2 pr-10 rounded-lg glass-input" />
                    <button type="button" @click="showPassword = !showPassword"
                        class="absolute right-3 top-1/2 -translate-y-1/2 text-brand">
                        <svg v-if="!showPassword" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8Z" />
                            <circle cx="12" cy="12" r="3" />
                        </svg>
                        <svg v-else xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                            <path d="M9.88 9.88a3 3 0 1 0 4.24 4.24" />
                            <path d="M10.73 5.08A10.43 10.43 0 0 1 12 5c7 0 11 8 11 8a18.5 18.5 0 0 1-2.16 3.19m-6.72-1.07a3 3 0 1 1-4.24-4.24" />
                            <path d="M2 2l20 20" />
                            <path d="M6.61 6.61A18.5 18.5 0 0 0 1 12s4 8 11 8a10.4 10.4 0 0 0 5.39-1.61" />
                        </svg>
                    </button>
                </div>
            </div>
            <button type="submit" class="w-full font-medium py-2 rounded-lg transition btn-brand">
                Login
            </button>
        </form>
        <p class="text-sm mt-4 text-center text-brand">
            Don't have an account?
            <router-link to="/signup" class="font-medium hover:underline text-brand">Sign Up</router-link>
        </p>
        <p class="text-sm mt-2 text-center">
            <router-link to="/reset-password" class="font-bold hover:underline text-brand">Forgot Password 🤔</router-link>
        </p>
    </div>
</template>

<script lang="ts">
import { login } from '../services/api';
import { showToast } from '../store';

export default {
  name: 'LoginForm',
  data() {
    return {
      email: '',
      password: '',
      showPassword: false
    };
  },
  methods: {
    async handleLogin() {
      const result = await login(this.email, this.password);

      if (result.token) {
        localStorage.setItem('authToken', result.token);
        showToast('Logged in successfully!', 'success');
        this.$emit('login-success', result.token);
      } else {
        showToast('Invalid email or password', 'error');
      }
    }
  }
};
</script>