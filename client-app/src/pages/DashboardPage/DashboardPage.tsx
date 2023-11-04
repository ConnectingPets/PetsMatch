import React from 'react';
import './DashboardPage.scss';

const pets = [
    {
        name: "Purrsloud",
        photo: "https://learnwebcode.github.io/json-example/images/cat-2.jpg"
    },
    {
        name: "Barksalot",
        photo: "https://learnwebcode.github.io/json-example/images/dog-1.jpg"
    },
    {
        name: "Meowsalot",
        photo: "https://learnwebcode.github.io/json-example/images/cat-1.jpg"
    }
]

interface DashboardPageProps { }

export const DashboardPage: React.FC<DashboardPageProps> = () => {
    return (
        <section className='dashboard__wrapper'>

            <article className='dashboard__greet'>
                <h2>welcome, jim carrey !</h2>
            </article>

            <article className='dashboard__article '>
                <h3>my pets:</h3>
                <section className='dashboard__pets'>
                    { pets.map(x=><img src={x.photo} key={x.name}/>)}
                </section>
            </article>

            <article className='dashboard__article'>
                <h3>my profile:</h3>
                <section className='dashboard__user'>

                </section>

            </article>

        </section>
    )
}
