import { makeAutoObservable, observable, action } from 'mobx';

import { IUser } from '../interfaces/Interfaces';

class UserStore {
    user: IUser | null = null;
    authToken: string | null = null;
    isLoggedIn: boolean = false;

    constructor() {
        this.loadFromStorage();

        makeAutoObservable(this, {
            user: observable,
            authToken: observable,
            isLoggedIn: observable,
            setUser: action,
            clearUser: action
        });
    }

    setIsLoggedIn() {
        this.isLoggedIn = true;

        localStorage.setItem('isLogged', JSON.stringify(this.isLoggedIn));
    }

    setUser(userData: IUser, authToken: string) {
        this.user = userData;
        this.authToken = authToken;

        this.saveToStorage();
    }

    clearUser() {
        this.user = null;
        this.authToken = null;
        this.isLoggedIn = false;

        this.removeFromStorage();
    }

    loadFromStorage() {
        const storedUser = localStorage.getItem('user');
        const storedToken = localStorage.getItem('token');
        const storedIsLoggedIn = localStorage.getItem('isLoggedIn');

        if (storedIsLoggedIn) {
            this.isLoggedIn = JSON.parse(storedIsLoggedIn);
        }

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
        localStorage.removeItem('isLogged');
    }
}

const userStore = new UserStore();
export default userStore;