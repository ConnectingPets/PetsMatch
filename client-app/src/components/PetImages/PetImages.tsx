import React, { useState } from 'react';
import { FieldInputProps } from 'react-final-form';

import { IAnimal } from '../../interfaces/Interfaces';

import { CLabel } from '../common/CLabel/CLabel';
import { CgAsterisk } from 'react-icons/cg';

import '../../global-styles/forms-images.scss';

interface PetImagesProps {
    errors: IAnimal | null
    input: FieldInputProps<File[], HTMLElement>
}

const PetImages: React.FC<PetImagesProps> = ({ errors, input }) => {
    const [images, setImages] = useState<string[]>([]);

    const handleFile = (
        e: React.ChangeEvent<HTMLInputElement>,
        input: FieldInputProps<File[], HTMLElement>
    ) => {
        const file = e.target.files?.[0];

        if (file) {
            const imageUrl = URL.createObjectURL(file);

            setImages([...images, imageUrl]);
            input.onChange([...input.value, file]);
        }
    };

    const handleRemoveImage = (index: number, input: FieldInputProps<File[], HTMLElement>) => {
        const updateImages = [...images];
        updateImages.splice(index, 1);
        setImages(updateImages);

        const updateFiles = [...input.value];
        updateFiles.splice(index, 1);
        input.onChange(updateFiles);
    };

    return (
        <>
            {images.length < 6 && (
                <>
                    <div className="required">
                        <CLabel inputName='Photo' title='Photo' />
                        <CgAsterisk className="asterisk" />
                    </div>

                    <div id="fileInput">
                        <input type="file" accept="image/*" multiple onChange={(e) => handleFile(e, input)} name='Photo' id='Photo' />
                        <p className="fakeFileInput" >Upload Photo</p>
                    </div>
                </>
            )}

            {images.length > 5 && (
                <p className="limit-message">Only up to 6 images can be uploaded.</p>
            )}

            {errors && <span>{errors.Photo}</span>}

            {images.length > 0 && (
                <div id="images">
                    {images.map((imageUrl, index) => (
                        <div key={index} className="image-preview">
                            <img src={imageUrl} alt="preview image" />
                            <button type="button" onClick={() => handleRemoveImage(index, input)}>X</button>
                        </div>
                    ))}
                </div>
            )}
        </>
    );
};

export default PetImages;