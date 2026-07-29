<template>
    <div class="min-h-screen w-full flex items-center justify-center px-6 app-gradient-bg">
        <div class="w-full max-w-sm mx-auto p-6 rounded-2xl glass-card">
            <h2 class="text-xl font-semibold mb-4 text-brand text-center">Reset Password</h2>
            <form @submit.prevent="handleReset" class="space-y-4">
                <div>
                    <label for="resetEmail" class="block text-sm font-medium mb-1 text-brand">Email</label>
                    <input id="resetEmail" type="email" v-model="email" required
                        class="w-full px-3 py-2 rounded-lg glass-input" />
                </div>
                <div>
                    <label for="newPassword" class="block text-sm font-medium mb-1 text-brand">New Password</label>
                    <div class="relative">
                        <input :type="showPassword ? 'text' : 'password'" id="newPassword" v-model="newPassword" required
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
                <div>
                    <label for="confirmNewPassword" class="block text-sm font-medium mb-1 text-brand">Confirm New Password</label>
                    <div class="relative">
                        <input :type="showConfirmPassword ? 'text' : 'password'" id="confirmNewPassword" v-model="confirmPassword" required
                            class="w-full px-3 py-2 pr-10 rounded-lg glass-input" />
                        <button type="button" @click="showConfirmPassword = !showConfirmPassword"
                            class="absolute right-3 top-1/2 -translate-y-1/2 text-brand">
                            <svg v-if="!showConfirmPassword" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
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
                    Reset Password
                </button>
            </form>
            <p class="text-sm mt-4 text-center text-brand">
                <router-link to="/" class="font-medium hover:underline text-brand">Back to Login</router-link>
            </p>
        </div>
    </div>
</template>

<script lang="ts">
import { resetPassword } from '../services/api';
import { showToast } from '../store';

export default {
    name: 'ResetPasswordView',
    data() {
        return {
            email: '',
            newPassword: '',
            confirmPassword: '',
            showPassword: false,
            showConfirmPassword: false
        };
    },
    methods: {
        async handleReset() {
            if (this.newPassword !== this.confirmPassword) {
                showToast('Passwords do not match', 'error');
                return;
            }

            const result = await resetPassword(this.email, this.newPassword);

            if (result.message === 'Password updated successfully') {
                showToast('Password reset successfully!', 'success');
                setTimeout(() => {
                    this.$router.push('/');
                }, 1500);
            } else {
                showToast(result.message || 'Something went wrong', 'error');
            }
        }
    }
};
</script>