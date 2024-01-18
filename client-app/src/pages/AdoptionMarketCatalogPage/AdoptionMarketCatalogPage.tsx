import React, { useEffect, useState } from 'react';
import { Field, Form } from 'react-final-form';
import { observer } from 'mobx-react';

import themeStore from '../../stores/themeStore';
import { Breeds, Categories, IUserAnimals } from '../../interfaces/Interfaces';
import agent from '../../api/axiosAgent';

import FormsHeader from '../../components/FormsHeader/FormsHeader';
import { CLabel } from '../../components/common/CLabel/CLabel';
import { CPetCard } from '../../components/common/CPetCard/CPetCard';
import Footer from '../../components/Footer/Footer';
import './AdoptionMarketCatalogPage.scss';

interface AdoptionMarketCatalogPageProps { }

const AdoptionMarketCatalogPage: React.FC<AdoptionMarketCatalogPageProps> = observer(() => {
    const [isMarket, setIsMarket] = useState<boolean>(true);
    const [petsInMarket, setPetsInMarket] = useState<IUserAnimals[]>([]);
    const [petsForAdoption, setPetsForAdoption] = useState<IUserAnimals[]>([]);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [itemsPerPage] = useState<number>(12);
    const [categories, setCategories] = useState<Categories[]>([]);
    const [breeds, setBreeds] = useState<Breeds[]>([]);

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = isMarket
        ? petsInMarket.slice(indexOfFirstItem, indexOfLastItem)
        : petsForAdoption.slice(indexOfFirstItem, indexOfLastItem);

    const pages = Math.ceil((isMarket ? petsInMarket.length : petsForAdoption.length) / itemsPerPage);

    const paginationButtons = () => {
        const pageButtons = [];

        for (let i = 1; i <= pages; i++) {
            pageButtons.push(
                <button key={i} onClick={() => setCurrentPage(i)} className={i == currentPage ? 'current-btn' : ''}>
                    {i}
                </button>
            );
        }

        return pageButtons;
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const allPetsInMarketResult = await agent.apiMarketplace.getAllAnimalsInMarketplace();
                setPetsInMarket(allPetsInMarketResult.data);

                const allPetsForAdoptionResult = await agent.apiAdoption.getAllAnimalsForAdoption();
                setPetsForAdoption(allPetsForAdoptionResult.data);
            } catch (err) {
                console.error(err);
            }
        };

        fetchData();
    }, []);

    const onClickAdoptionMarket = () => {
        setIsMarket(state => !state);

        setCurrentPage(1);
    };

    useEffect(() => {
        agent.apiAnimal.getAllCategories()
            .then(res => {
                setCategories(res.data);
            });
    }, []);

    const onSearchSubmit = async (values: any) => {
        console.log(values);

        if (values.AnimalCategory) {
            const res = await agent.apiAnimal.getAllBreeds(Number(values.AnimalCategory));

            setBreeds(res.data);
        }
    };

    return (
        <div className={themeStore.isLightTheme ? 'catalog-wrapper' : 'catalog-wrapper catalog-wrapper__dark'}>
            <FormsHeader title='Pets Catalog' />

            <article className={themeStore.isLightTheme ? 'catalog-wrapper__article ' : 'catalog-wrapper__article catalog-wrapper__article__dark '}>
                <section className='catalog-wrapper__article__options'>
                    <div>
                        <h3 className={isMarket ? 'catalog-wrapper__article__option' : ''} onClick={onClickAdoptionMarket}>Marketplace</h3>
                    </div>
                    <div>
                        <h3 className={!isMarket ? 'catalog-wrapper__article__option' : ''} onClick={onClickAdoptionMarket}>For Adoption</h3>
                    </div>
                </section>

                <section className='catalog-wrapper__article__search'>
                    <h3>Search by</h3>
                    <Form
                        onSubmit={onSearchSubmit}
                        render={({ handleSubmit }) => (
                            <form onChange={handleSubmit}>

                                <Field name='AnimalCategory'>
                                    {({ input }) => (
                                        <>
                                            <CLabel inputName='AnimalCategory' title='Category' />
                                            <select {...input} name="AnimalCategory" id="AnimalCategory">
                                                <option>  </option>
                                                {categories && categories.map(c => <option value={c.animalCategoryId} key={c.animalCategoryId}>{c.name}</option>)}
                                            </select>
                                        </>
                                    )}
                                </Field>

                                <Field name='BreedId'>
                                    {({ input }) => (
                                        <>
                                            <CLabel inputName='BreedId' title='Breed' />
                                            <select {...input} name="BreedId.breedId" id="Breed">
                                                <option>  </option>
                                                {categories && breeds.map(b => <option value={b.breedId} key={b.breedId}>{b.name}</option>)}
                                            </select>
                                        </>
                                    )}
                                </Field>

                                <Field name='Gender'>
                                    {({ input }) => (
                                        <>
                                            <CLabel inputName='Gender' title='Gender' />
                                            <select {...input} name="Gender" id="Gender">
                                                <option>  </option>
                                                <option>Male</option>
                                                <option>Female</option>
                                            </select>
                                        </>
                                    )}
                                </Field>

                                <Field name='City'>
                                    {({ input }) => (
                                        <>
                                            <CLabel inputName='City' title='City' />
                                            <select {...input} name="City" id="City">
                                                <option>  </option>
                                                <option>Pernik</option>
                                                <option>Svishtov</option>
                                                <option>Chicago</option>
                                            </select>
                                        </>
                                    )}
                                </Field>

                            </form>
                        )} />
                </section>

                <section className='catalog-wrapper__pets'>
                    {!isMarket && currentItems.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='catalogAdoption' key={x.id} />)}

                    {isMarket && currentItems.map(x => <CPetCard name={x.name} photo={x.mainPhoto} id={x.id} buttons='catalogMarket' key={x.id} />)}
                </section>

                <div className='catalog-wrapper__pagination-btn'>{paginationButtons()}</div>
            </article>

            <Footer />
        </div>
    );
});

export default AdoptionMarketCatalogPage;