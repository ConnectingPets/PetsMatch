import React from 'react';

import './About.scss';

interface AboutProps { }

const About: React.FC<AboutProps> = () => {
    return (
        <div className="info__container__about">
            <h2>About Us</h2>

            <div className="info__container__about__content">
                <img src="/favicon.ico" alt="footprint" />

                <div className="info__container__about__content__text">
                    <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Illo, dolore vel. Earum blanditiis saepe accusamus magni placeat quisquam sint error quibusdam cupiditate maxime aspernatur, veniam quaerat culpa, eum voluptates quod modi eos nobis nam obcaecati. Quibusdam ipsa odit quidem laboriosam, nostrum neque quam error omnis aliquam ipsam. Adipisci vero delectus, ex ducimus quam voluptatum consequatur repellendus facere earum officiis corporis dolore! Sequi error praesentium quos voluptates inventore consequatur, nesciunt est totam reprehenderit quibusdam ab repellat aut nulla quisquam libero voluptate temporibus voluptatem doloribus facilis iure eius assumenda, accusamus debitis? Pariatur ipsa nulla doloribus, soluta omnis reprehenderit esse corporis repellendus molestiae dolorum earum, itaque illo ducimus. In non at cum provident praesentium error ipsum, quam molestiae commodi voluptatum perspiciatis quidem aliquam? Amet mollitia debitis deleniti harum repellat quia distinctio possimus alias reiciendis sunt sapiente expedita laboriosam dolorem odio, iusto, aspernatur est cum placeat, obcaecati assumenda veniam? Corrupti odit aliquid reiciendis exercitationem animi eius eos architecto laborum in cupiditate fuga consequatur optio recusandae earum saepe, laudantium aliquam iusto quidem necessitatibus fugiat voluptate dicta. Officiis itaque a dolor reprehenderit rerum alias odio cumque asperiores impedit incidunt, ad illum, rem saepe possimus, numquam corrupti. Reiciendis voluptatibus hic doloremque cupiditate qui, dolores maiores consequatur atque deserunt rerum alias! Quam error magnam ipsam modi adipisci, nemo tempore autem provident totam. Eos rerum dignissimos suscipit harum dolorem necessitatibus quis veniam ipsam facilis debitis, porro, iste nesciunt qui fugit aliquam id? Ex reprehenderit earum ad ipsam voluptate quo, maiores dicta molestias qui blanditiis soluta eum, placeat obcaecati quod.</p>
                </div>
            </div>
        </div>
    );
};

export default About;