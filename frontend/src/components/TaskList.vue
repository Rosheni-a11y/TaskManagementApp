<template>
    <div class="rounded-2xl p-5 glass-card">
        <h2 class="text-lg font-semibold mb-3 text-brand">Tasks</h2>
        <div class="flex flex-row gap-3 mb-4">
            <input type="text" :value="searchQuery" @input="searchQuery = $event.target.value"
                placeholder="Search tasks..."
                class="flex-[2] min-w-0 px-3 py-2 rounded-lg glass-input" />
            <select v-model="statusFilter"
                class="flex-1 min-w-0 px-2 py-2 rounded-lg glass-input text-brand text-sm">
                <option value="">All</option>
                <option value="Pending">Pending</option>
                <option value="In Progress">In Progress</option>
                <option value="Completed">Completed</option>
            </select>
        </div>
        <ul class="space-y-2">
            <li v-for="task in filteredTasks" :key="task.id"
                class="flex items-center justify-between gap-3 rounded-lg px-4 py-3"
                style="background: rgba(255,255,255,0.3); border: 1px solid rgba(255,255,255,0.4);">
                <div class="flex-1">
                    <p class="font-medium text-brand">{{ task.title }}</p>
                    <span class="text-xs" style="color: #41431B; opacity: 0.7;">{{ task.status }}</span>
                </div>
                <button @click="openEditModal(task)" class="p-2 rounded-lg text-brand" title="Edit task">
                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                        <path d="M17 3a2.828 2.828 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5L17 3Z" />
                    </svg>
                </button>
                <button @click="handleDeleteTask(task.id)" class="p-2 rounded-lg" style="color: #b91c1c;" title="Delete task">
                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                        <path d="M3 6h18" />
                        <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" />
                        <line x1="10" y1="11" x2="10" y2="17" />
                        <line x1="14" y1="11" x2="14" y2="17" />
                    </svg>
                </button>
            </li>
        </ul>
        <p v-if="tasks.length === 0" class="text-sm text-center py-4" style="color: #41431B; opacity: 0.6;">No tasks yet</p>
        <p v-else-if="filteredTasks.length === 0" class="text-sm text-center py-4" style="color: #41431B; opacity: 0.6;">No tasks match your search</p>

        <!-- Edit Modal -->
        <div v-if="editingTask" class="fixed inset-0 z-40 flex items-center justify-center px-4"
            style="background: rgba(65, 67, 27, 0.4);" @click.self="closeEditModal">
            <div class="w-full max-w-sm rounded-2xl p-6 glass-card" style="background: rgba(248, 243, 225, 0.95); backdrop-filter: blur(24px);">
                <h3 class="text-lg font-semibold mb-4 text-brand">Edit Task</h3>
                <form @submit.prevent="handleSaveEdit" class="space-y-3">
                    <div>
                        <label class="block text-sm font-medium mb-1 text-brand">Title</label>
                        <input type="text" v-model="editForm.title" required
                            class="w-full px-3 py-2 rounded-lg glass-input" />
                    </div>
                    <div>
                        <label class="block text-sm font-medium mb-1 text-brand">Description</label>
                        <input type="text" v-model="editForm.description"
                            class="w-full px-3 py-2 rounded-lg glass-input" />
                    </div>
                    <div>
                        <label class="block text-sm font-medium mb-1 text-brand">Due Date</label>
                        <input type="date" v-model="editForm.dueDate"
                            class="w-full px-3 py-2 rounded-lg glass-input text-brand" />
                    </div>
                    <div>
                        <label class="block text-sm font-medium mb-1 text-brand">Status</label>
                        <select v-model="editForm.status" class="w-full px-3 py-2 rounded-lg glass-input text-brand">
                            <option value="Pending">Pending</option>
                            <option value="In Progress">In Progress</option>
                            <option value="Completed">Completed</option>
                        </select>
                    </div>
                    <div class="flex gap-3 mt-4">
                        <button type="button" @click="closeEditModal"
                            class="flex-1 py-2 rounded-lg font-medium text-brand"
                            style="background: rgba(255,255,255,0.4); border: 1px solid rgba(255,255,255,0.5);">
                            Cancel
                        </button>
                        <button type="submit" class="flex-1 py-2 rounded-lg font-medium btn-brand">
                            Save
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { getTasksByUserId, deleteTask, updateTask } from '../services/api';
    import { showToast } from '../store';

    export default {
        name: 'TaskList',
        props: {
            userId: {
                type: [String, Number],
                required: true
            }
        },
        data() {
            return {
                tasks: [] as any[],
                searchQuery: '',
                statusFilter: '',
                editingTask: null as any,
                editForm: {
                    title: '',
                    description: '',
                    dueDate: '',
                    status: 'Pending'
                }
            };
        },
        computed: {
            filteredTasks() {
                let result = this.tasks;

                if (this.searchQuery.trim()) {
                    const query = this.searchQuery.toLowerCase();
                    result = result.filter((task: any) =>
                        task.title.toLowerCase().includes(query)
                    );
                }

                if (this.statusFilter) {
                    result = result.filter((task: any) => task.status === this.statusFilter);
                }

                return result;
            }
        },
        async mounted() {
            await this.fetchTasks(this.userId);
        },
        watch: {
            userId(newUserId: string | number) {
                this.fetchTasks(newUserId);
            }
        },
        methods: {
            async fetchTasks(userId: string | number) {
                if (!userId) {
                    this.tasks = [];
                    this.$emit('tasks-loaded', []);
                    return;
                }
                this.tasks = await getTasksByUserId(Number(userId));
                this.$emit('tasks-loaded', this.tasks);
            },
            async handleDeleteTask(taskId: any) {
                const token = localStorage.getItem('authToken');
                await deleteTask(taskId, token);
                showToast('Task deleted', 'success');
                this.fetchTasks(this.userId);
            },
            openEditModal(task: any) {
                this.editingTask = task;
                this.editForm = {
                    title: task.title,
                    description: task.description || '',
                    dueDate: task.dueDate ? task.dueDate.split('T')[0] : '',
                    status: task.status
                };
            },
            closeEditModal() {
                this.editingTask = null;
            },
            async handleSaveEdit() {
                const token = localStorage.getItem('authToken');
                await updateTask(this.editingTask.id, {
                    title: this.editForm.title,
                    description: this.editForm.description,
                    status: this.editForm.status,
                    dueDate: this.editForm.dueDate || null,
                    userId: Number(this.userId)
                }, token as string);

                showToast('Task updated successfully!', 'success');
                this.closeEditModal();
                this.fetchTasks(this.userId);
            }
        }
    };
</script>