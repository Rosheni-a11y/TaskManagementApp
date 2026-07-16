<template>
    <div>
        <label for="user-select">Select User:</label>
        <select id="user-select" v-model="selectedUserId" @change="onUserChange">
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
    this.users = await getUsers();
  },
  methods: {
    onUserChange() {
      this.$emit('user-selected', this.selectedUserId);
    }
  }
};
</script>