import { defineStore } from 'pinia'
import { type Project } from "@/models/Project";
import { useHttp } from '@/composables/http';
import { reactive, ref } from 'vue';
import { useStore } from "@/stores";
import StoreImpl from './storeImpl';

const http = useHttp();
const store = useStore();

const projects = reactive(new Array<Project>());

const implementation = new StoreImpl<Project>("project", projects);

const loadProjects = async () => await implementation.loadItems(); 
const getProject = async (id: number) => await implementation.getItem(id); 
const saveProject = async (project: Project) => await implementation.saveItem(project);
const updateProject = async (project: Project) => await implementation.updateItem(project);
const deleteProject = async (id: number) => await implementation.deleteItem(id);

export const useProjectStore = defineStore('projects', () => {

  return {
    projects,
    loadProjects,
    getProject,
    saveProject,
    updateProject,
    deleteProject
  };
})
