import { action, makeAutoObservable, observable } from "mobx";

class ThemeStore {
    isLightTheme: boolean = true;

    constructor() {
        makeAutoObservable(this, {
            isLightTheme: observable,
            changeTheme: action
        })
    }

    changeTheme() {
        console.log(this.isLightTheme);
        
        this.isLightTheme = !this.isLightTheme
    }
}

const themeStore = new ThemeStore();
export default themeStore;