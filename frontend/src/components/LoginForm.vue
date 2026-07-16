<template>
    <div>
        <h2>Login</h2>
        <form @submit.prevent="handleLogin">
            <div>
                <label for="email">Email:</label>
                <input id="email" type="email" v-model="email" required />
            </div>
            <div>
                <label for="password">Password:</label>
                <input id="password" type="password" v-model="password" required />
            </div>
            <button type="submit">Login</button>
        </form>
        <p v-if="errorMessage" style="color: red;">{{ errorMessage }}</p>
        <p v-if="isLoggedIn" style="color: green;">Logged in successfully!</p>
    </div>
</template>

<script lang="ts">
import { login } from '../services/api';

export default {
  name: 'LoginForm',
  data() {
    return {
      email: '',
      password: '',
      errorMessage: '',
      isLoggedIn: false
    };
  },
  methods: {
    async handleLogin() {
      this.errorMessage = '';
      const result = await login(this.email, this.password);

      if (result.token) {
        localStorage.setItem('authToken', result.token);
        this.isLoggedIn = true;
        this.$emit('login-success', result.token);
      } else {
        this.errorMessage = 'Invalid email or password';
      }
    }
  }
};
</script>