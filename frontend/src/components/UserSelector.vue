<template>
    <div class="rounded-2xl p-5 mb-4 glass-card">
        <label for="user-select" class="block text-lg font-bold mb-2 text-brand">Select User</label>
        <select id="user-select" v-model="selectedUserId" @change="onUserChange"
            class="w-full px-3 py-2 rounded-lg glass-input text-brand">
            <option value="">-- Select a user --</option>
            <option v-for="user in users" :key="user.id" :value="user.id">
                {{ user.name }}
            </option>
        </select>
    </div>
</template>

<script lang="ts">
import { getUsers } from '../services/api';

export default {
  name: 'UserSelector',
  data() {
    return {
      users: [] as any[],
      selectedUserId: ''
    };
  },
  async mounted() {
    await this.fetchUsers();
  },
  methods: {
    async fetchUsers() {
      this.users = await getUsers();
    },
    onUserChange() {
      this.$emit('user-selected', this.selectedUserId);
    }
  }
};
</script>