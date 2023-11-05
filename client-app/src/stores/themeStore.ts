import { action, makeAutoObservable, observable } from "mobx";

class ThemeStore {
    isLightTheme: boolean = true;

    constructor() {
        makeAutoObservable(this,{
            isLightTheme:observable,
            changeTheme:action
        })
    }

    changeTheme() {
        this.isLightTheme = !this.isLightTheme
        console.log(this.isLightTheme);  
    }
}

const themeStore = new ThemeStore();
export default themeStore;