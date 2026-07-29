<template>
    <div class="min-h-screen w-full flex items-center justify-center px-6 app-gradient-bg">
        <div class="w-full max-w-sm">
            <TaskForm :userId="store.selectedUserId" @task-created="handleTaskCreated" />
            <p class="text-sm mt-2 text-center text-brand">
                <router-link to="/dashboard" class="font-medium hover:underline text-brand">Back to Dashboard</router-link>
            </p>
        </div>
    </div>
</template>

<script lang="ts">
import TaskForm from '../components/TaskForm.vue';
import { store, showToast } from '../store';

export default {
    name: 'AddTaskView',
    components: { TaskForm },
    data() {
        return { store };
    },
    methods: {
        handleTaskCreated() {
            showToast('Task added successfully!', 'success');
            this.$router.push('/dashboard');
        }
    }
};
</script>