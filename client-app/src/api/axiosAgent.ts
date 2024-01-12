import axios, { AxiosResponse } from 'axios';

import { IUser } from '../interfaces/Interfaces';
import { IAnimal } from '../interfaces/Interfaces';

const axiosInstance = axios.create({
    baseURL: 'http://localhost:5216',
    withCredentials: true
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
    editUser: (body: IUser) => requests.patch('/api/profile/edit', body, headers.appJSON),
    deleteUser: () => requests.del('/api/profile/delete')
};

const apiAnimal = {
    getAllAnimals: () => requests.get('/api/animal/allanimals'),
    getAllCategories: () => requests.get('/api/animal/GetAllCategories'),
    getAllBreeds: (categoryId: number) => requests.get(`/api/animal/GetBreeds/${categoryId}`),
    getAnimalById: (animalId: string) => requests.get(`/api/animal/editanimal/${animalId}`),
    addAnimal: (animalData: FormData) => requests.post('/api/animal/add', animalData, headers.multipart),
    editAnimalById: (animalId: string, animalData: IAnimal) => requests.patch(`/api/animal/${animalId}`, animalData, headers.multipart),
    deleteAnimal: (animalId: string) => requests.del(`/api/animal/${animalId}`),
    uploadAnimalPhoto: (animalId: string, image: FormData) => requests.post(`/api/photo/AddAnimalPhoto/${animalId}`, image, headers.multipart),
    setAnimalMainPhoto: (photoId: string, data: object) => requests.post(`/api/photo/SetAnimalMain/${photoId}`, data, headers.multipart),
    deleteAnimalPhoto: (photoId: string) => requests.del(`/api/photo/DeleteAnimalPhoto/${photoId}`)
};

const apiMatches = {
    getAllPossibleSwipesForAnimal: (animalId: string) => requests.get(`/api/swipe/animals?animalId=${animalId}`),
    animalMatches: (animalId: string) => requests.get(`/animal-matches?animalId=${animalId}`),
    swipe: (swiperAnimalId: string, swipeeAnimalId: string, swipedRight: boolean) => requests.post('/swipe', { swiperAnimalId, swipeeAnimalId, swipedRight }, headers.appJSON),
    match: (animalOneId: string, animalTwoId: string) => requests.post('/match', { animalOneId, animalTwoId }, headers.appJSON),
    getPetProfile: (animalId: string) => requests.get(`api/animal/profile/${animalId}`),
    unmatch: (animalOneId: string, animalTwoId: string) => requests.post('/unmatch', { animalOneId, animalTwoId }, headers.appJSON)
};

const apiPhotos = {
    addUserPhoto: (body: FormData) => requests.post('/api/photo/addUserPhoto', body, headers.multipart),
    deleteUserPhoto: () => requests.del('/api/photo/deleteUserPhoto')
};

const apiMessages = {
    getChatHistory: (matchId: string) => requests.get(`/chatHistory?matchId=${matchId}`),
    sendMessage: (body: object) => requests.post('/sendMessage', body, headers.appJSON)
};

const apiMarketplace = {
    getAllAnimalsInMarketplace: () => requests.get('/api/marketplace/AllAnimalsForSale'),
    getMyAnimalsForSale: () => requests.get('/api/marketplace/MyAnimalsForSale'),
    getAnimalById: (animalId: string) => requests.get(`/api/marketplace/EditAnimal/${animalId}`),
    addAnimal: (animalData: FormData) => requests.post('/api/marketplace/Add', animalData, headers.multipart),
    editAnimalById: (animalId: string, animalData: IAnimal) => requests.patch(`/api/marketplace/${animalId}`, animalData, headers.multipart)
};

const apiAdoption = {
    getAllAnimalsForAdoption: () => requests.get('/api/marketplace/AllAnimalsForAdoption'),
    getMyAnimalsForAdoption: () => requests.get('/api/marketplace/MyAnimalsForAdoption')
};

const agent = {
    apiUser,
    apiAnimal,
    apiMatches,
    apiPhotos,
    apiMessages,
    apiMarketplace,
    apiAdoption
};

export default agent;