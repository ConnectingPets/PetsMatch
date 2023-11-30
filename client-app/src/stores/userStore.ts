import { makeAutoObservable, observable, action } from 'mobx';

import { IUser } from '../interfaces/Interfaces';

class UserStore {
    user: IUser | null = null;
    authToken: string | null = null;

    constructor() {
        this.loadFromStorage();

        makeAutoObservable(this, {
            user: observable,
            authToken: observable,
            setUser: action,
            clearUser: action
        });
    }

    setUser(userData: IUser, authToken: string) {
        this.user = userData;
        this.authToken = authToken;

        this.saveToStorage();
    }

    clearUser() {
        this.user = null;
        this.authToken = null;

        this.removeFromStorage();
    }

    loadFromStorage() {
        const storedUser = localStorage.getItem('user');
        const storedToken = localStorage.getItem('token');

        if (storedUser && storedToken) {
            this.user = JSON.parse(storedUser);
            this.authToken = storedToken;
        }
    }

    saveToStorage() {
        if (this.authToken) {
            localStorage.setItem('user', JSON.stringify(this.user));
            localStorage.setItem('token', this.authToken);
        }
    }

    removeFromStorage() {
        localStorage.removeItem('user');
        localStorage.removeItem('token');
    }
}

const userStore = new UserStore();
export default userStore;