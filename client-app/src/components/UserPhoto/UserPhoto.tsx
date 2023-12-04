import React, { useState } from 'react';
import { FieldInputProps } from 'react-final-form';

import { CLabel } from '../common/CLabel/CLabel';

import '../../global-styles/forms-images.scss';

interface UserPhotoProps {
    input: FieldInputProps<File, HTMLElement>
}

const UserPhoto: React.FC<UserPhotoProps> = ({ input }) => {
    const [photo, setPhoto] = useState<string | null>(null);

    const handleFile = (
        e: React.ChangeEvent<HTMLInputElement>,
        input: FieldInputProps<File, HTMLElement>
    ) => {
        const file = e.target.files?.[0];

        if (file) {
            const imageUrl = URL.createObjectURL(file);

            setPhoto(imageUrl);
            input.onChange(file);
        }
    };

    const handleRemovePhoto = (input: FieldInputProps<File, HTMLElement>) => {
        setPhoto(null);
        input.onChange(null);
    };

    return (
        <>
            {!photo && (
                <>
                    <CLabel inputName='Photo' title='Photo' />
                    <div id="fileInput">
                        <input type="file" accept="image/*" onChange={(e) => handleFile(e, input)} name='Photo' id='Photo' />
                        <p className="fakeFileInput" >Upload Photo</p>
                    </div>
                </>
            )}

            {photo && (
                <div id="images">
                    <div className="image-preview">
                        <img src={photo} alt="preview image" />
                        <button type="button" onClick={() => handleRemovePhoto(input)}>X</button>
                    </div>
                </div>
            )}
        </>
    );
};

export default UserPhoto;