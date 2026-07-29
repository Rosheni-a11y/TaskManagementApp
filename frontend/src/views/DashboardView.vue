<template>
    <div class="min-h-screen w-full app-gradient-bg px-4 py-6">
        <div class="max-w-xl mx-auto">
            <UserSelector ref="userSelector" @user-selected="handleUserSelected" />

            <div v-if="selectedUserId" class="grid grid-cols-2 gap-3 mb-4">
                <div class="rounded-2xl p-4 glass-card text-center">
                    <p class="text-2xl font-bold text-brand">{{ stats.all }}</p>
                    <p class="text-sm text-brand">All Tasks</p>
                </div>
                <div class="rounded-2xl p-4 glass-card text-center">
                    <p class="text-2xl font-bold text-brand">{{ stats.pending }}</p>
                    <p class="text-sm text-brand">Pending</p>
                </div>
                <div class="rounded-2xl p-4 glass-card text-center">
                    <p class="text-2xl font-bold text-brand">{{ stats.inProgress }}</p>
                    <p class="text-sm text-brand">In Progress</p>
                </div>
                <div class="rounded-2xl p-4 glass-card text-center">
                    <p class="text-2xl font-bold text-brand">{{ stats.completed }}</p>
                    <p class="text-sm text-brand">Completed</p>
                </div>
            </div>

            <router-link v-if="selectedUserId" to="/add-task"
                class="block w-full text-center font-medium py-2 rounded-lg transition btn-brand mb-4">
                + Add Task
            </router-link>

            <TaskList v-if="selectedUserId" :userId="selectedUserId" ref="taskList" @tasks-loaded="handleTasksLoaded" />
        </div>
    </div>
</template>

<script lang="ts">
import UserSelector from '../components/UserSelector.vue';
import TaskList from '../components/TaskList.vue';
import { store } from '../store';

export default {
    name: 'DashboardView',
    components: { UserSelector, TaskList },
    data() {
        return {
            selectedUserId: store.selectedUserId,
            stats: {
                all: 0,
                pending: 0,
                inProgress: 0,
                completed: 0
            }
        };
    },
    methods: {
        handleUserSelected(userId: string) {
            this.selectedUserId = userId;
            store.selectedUserId = userId;
        },
        handleTasksLoaded(tasks: any[]) {
            this.stats.all = tasks.length;
            this.stats.pending = tasks.filter((t: any) => t.status === 'Pending').length;
            this.stats.inProgress = tasks.filter((t: any) => t.status === 'In Progress').length;
            this.stats.completed = tasks.filter((t: any) => t.status === 'Completed').length;
        }
    }
};
</script>