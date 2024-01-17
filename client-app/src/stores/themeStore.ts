import { action, makeAutoObservable, observable } from 'mobx';

class ThemeStore {
    localStorageTheme = localStorage.getItem('theme');
    isLightTheme: boolean = this.localStorageTheme != null ? JSON.parse(this.localStorageTheme) : true;

    constructor() {
        makeAutoObservable(this, {
            isLightTheme: observable,
            changeTheme: action
        });
    }

    changeTheme() {
        this.isLightTheme = !this.isLightTheme;
        localStorage.setItem('theme', JSON.stringify(this.isLightTheme));
    }
}

const themeStore = new ThemeStore();
export default themeStore;