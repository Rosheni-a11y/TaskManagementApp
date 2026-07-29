<template>
    <div class="min-h-screen w-full app-gradient-bg">
        <Navbar />
        <div class="px-4 py-6">
            <div class="max-w-xl mx-auto">
                <UserSelector
                    ref="userSelector"
                    @user-selected="handleUserSelected"
                />

                <!-- Statistics always visible -->
                <div class="grid grid-cols-2 gap-3 mb-4">
                    <div class="rounded-2xl p-4 glass-card text-center">
                        <p class="text-2xl font-bold text-brand">
                            {{ stats.all }}
                        </p>
                        <p class="text-sm text-brand">All Tasks</p>
                    </div>

                    <div class="rounded-2xl p-4 glass-card text-center">
                        <p class="text-2xl font-bold text-brand">
                            {{ stats.pending }}
                        </p>
                        <p class="text-sm text-brand">Pending</p>
                    </div>

                    <div class="rounded-2xl p-4 glass-card text-center">
                        <p class="text-2xl font-bold text-brand">
                            {{ stats.inProgress }}
                        </p>
                        <p class="text-sm text-brand">In Progress</p>
                    </div>

                    <div class="rounded-2xl p-4 glass-card text-center">
                        <p class="text-2xl font-bold text-brand">
                            {{ stats.completed }}
                        </p>
                        <p class="text-sm text-brand">Completed</p>
                    </div>
                </div>

                <TaskList
                    v-if="selectedUserId"
                    :userId="selectedUserId"
                    ref="taskList"
                    @tasks-loaded="handleTasksLoaded"
                />

                <p
                    v-else
                    class="text-sm text-center text-brand py-4"
                    style="opacity: 0.7;"
                >
                    Select a user to view their tasks
                </p>
            </div>
        </div>
    </div>
</template>


<script lang="ts">
import Navbar from '../components/Navbar.vue';
import UserSelector from '../components/UserSelector.vue';
import TaskList from '../components/TaskList.vue';
import { getAllTasks } from '../services/api';
import { store } from '../store';

export default {
    name: 'DashboardView',

    components: {
        Navbar,
        UserSelector,
        TaskList
    },

    data() {
        return {
            selectedUserId: store.selectedUserId || '',

            stats: {
                all: 0,
                pending: 0,
                inProgress: 0,
                completed: 0
            }
        };
    },

    async mounted() {
        await this.loadAllTaskStats();
    },

    methods: {
        async handleUserSelected(userId: string | number | null) {
            this.selectedUserId = userId || '';
            store.selectedUserId = userId ? String(userId) : '';

            if (!userId) {
                await this.loadAllTaskStats();
            }
        },

        async loadAllTaskStats() {
            try {
                const response = await getAllTasks();

                console.log('All tasks response:', response);

                const tasks = Array.isArray(response)
                    ? response
                    : response.data ?? response.tasks ?? [];

                this.updateStats(tasks);
            } catch (error) {
                console.error('Failed to load all tasks:', error);
            }
        },

        handleTasksLoaded(tasks: any[]) {
            this.updateStats(tasks);
        },

        updateStats(tasks: any[]) {
            this.stats.all = tasks.length;

            this.stats.pending = tasks.filter(
                (task: any) =>
                    task.status?.trim().toLowerCase() === 'pending'
            ).length;

            this.stats.inProgress = tasks.filter(
                (task: any) => {
                    const status = task.status
                        ?.trim()
                        .toLowerCase()
                        .replace(/\s+/g, '');

                    return status === 'inprogress';
                }
            ).length;

            this.stats.completed = tasks.filter(
                (task: any) => {
                    const status = task.status?.trim().toLowerCase();

                    return status === 'completed' || status === 'complete';
                }
            ).length;
        }
    }
};
</script>