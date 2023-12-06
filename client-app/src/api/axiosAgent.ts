import axios, { AxiosResponse } from 'axios';

import { IUser } from '../interfaces/Interfaces';
import { IAnimal } from '../interfaces/Interfaces';

const axiosInstance = axios.create({
    baseURL: 'http://localhost:5216',
});

const headers = {
    appJSON: { 'Content-Type': 'application/json' },
    multipart: { 'Content-Type': 'multipart/form-data' }
};

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
    get: (url: string) => axiosInstance.get(url).then(responseBody),
    post: (url: string, body: object, headers: object) => axiosInstance.post(url, body, headers).then(responseBody),
    patch: (url: string, body: object, headers: object) => axiosInstance.patch(url, body, headers).then(responseBody),
    del: (url: string) => axiosInstance.delete(url).then(responseBody)
};

const apiUser = {
    register: (userData: IUser) => requests.post('/api/user/register', userData, headers.appJSON),
    login: (userData: IUser) => requests.post('/api/user/login', userData, headers.appJSON),
    logout: (body: object) => requests.post('/api/user/logout', body, headers.appJSON),
    editUser: (body: IUser) => requests.post('/api/profile/edit', body, headers.appJSON),
    deleteUser: () => requests.post('/api/profile/delete', {}, headers.appJSON)
};

const apiAnimal = {
    getAllAnimals: () => requests.get('/api/animal/allanimals'),
    getAnimalById: (animalId: string) => requests.get(`/api/animal/editanimal/${animalId}`),
    // getAnimalCategoryAndBreed: () => requests.get('/api/animal/addanimal'),
    addAnimal: (animalData: IAnimal) => requests.post('/api/animal/add', animalData, headers.multipart),
    editAnimalById: (animalId: string, animalData: IAnimal) => requests.patch(`/api/animal/${animalId}`, animalData, headers.multipart),
    deleteAnimal: (animalId: string) => requests.del(`/api/animal/${animalId}`)
};

const apiMatches = {
    animalMatches: (animalId: string) => requests.get(`/animal-matches?animalId=${animalId}`),
    swipe: (swiperAnimalId: string, swipeeAnimalId: string, swipedRight: boolean) => requests.post('/swipe', { swiperAnimalId, swipeeAnimalId, swipedRight }, headers.appJSON),
    match: (animalOneId: string, animalTwoId: string) => requests.post('/match', { animalOneId, animalTwoId }, headers.appJSON),
    unmatch: (animalOneId: string, animalTwoId: string) => requests.post('/unmatch', { animalOneId, animalTwoId }, headers.appJSON)
};

const agent = {
    apiUser,
    apiAnimal,
    apiMatches
};

export default agent;