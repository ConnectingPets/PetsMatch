import { makeAutoObservable, observable, action } from 'mobx';

import { IUser } from '../interfaces/Interfaces';

class UserStore {
    user: IUser | null = null;
    authToken: string | null = null;

    constructor() {
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
    }

    clearUser() {
        this.user = null;
        this.authToken = null;
    }
}

const userStore = new UserStore();
export default userStore;