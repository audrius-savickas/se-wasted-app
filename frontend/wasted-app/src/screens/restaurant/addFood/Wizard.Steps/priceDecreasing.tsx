import React, {useState} from 'react'
import { Text } from 'react-native'
import { Checkbox, DateTimePicker, Incubator, Slider, Stepper, View } from 'react-native-ui-lib'
import { TextFieldProps } from "react-native-ui-lib/generatedTypes/src/incubator"
import { DecreaseType } from '../../../../api/interfaces'

let date:Date = new Date()

const textFieldCommonValues : TextFieldProps = {
    editable:false,
    centered: false
}

export const PriceDecreasing = () => {
    const [minimumPrice, setMinimumPrice] = useState<Number>(0)
    const [decreaseType, setDecreaseType] = useState<DecreaseType>(DecreaseType.AMOUNT)
    const [decreaseStep, setDecreaseStep] = useState<Number>(0)

    return (
        <View
            flex
            style={{
                flexDirection: "column",
                alignContent: "center",
                justifyContent: "space-around",
                width: '100%',
                height: "100%"
            }}
        >
            <View>
                <DateTimePicker
                    title="Start decreasing at"
                    mode="time"
                    display="default"
                    value={date}
                />
            </View>

            <View>
                <Text>Minimum price: {minimumPrice.toFixed(2)}</Text>
                <Slider 
                    minimumValue={0}
                    maximumValue={10}
                    onValueChange={setMinimumPrice}
                    value={10}
                    step={0.01}
                />  
            </View>

            <View>
                <Text>Decrease price interval (mins)</Text>
                <Stepper minValue={5} maxValue={180} step={5} small/>
            </View>

            <View>
                <View>
                    <Checkbox
                        value={decreaseType===DecreaseType.AMOUNT}
                        onValueChange={() => {
                            setDecreaseType(DecreaseType.AMOUNT)
                            setDecreaseStep(0)
                        }}
                        label="Amount"
                    />
                    <Checkbox
                        style={{
                            marginTop: "5%",
                            marginBottom: "5%"
                        }}
                        value={decreaseType===DecreaseType.PERCENT}
                        onValueChange={() => {
                            setDecreaseType(DecreaseType.PERCENT)
                            setDecreaseStep(0)
                        }}
                        label="Percent"
                    />   
                </View>
             

                <View>
                    <Text>
                        Decrease step: {decreaseStep.toFixed(2)} {
                            (decreaseType===DecreaseType.AMOUNT)
                            ? '€'
                            : '%'
                        }
                    </Text>
                    <Slider 
                        minimumValue={0}
                        maximumValue={
                            (decreaseType===DecreaseType.AMOUNT)
                            ? 10 
                            : 1
                        }
                        value={0}
                        step={0.01}
                        onValueChange={setDecreaseStep}
                    />
                </View> 
            </View> 
        </View>
    )
}