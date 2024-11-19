import serial
import sys
import os
from enum import Enum
import json
import time


class Commands(Enum):
    ON = "ON"
    OFF = "OFF"
    ERROR = None


def read_config() -> dict:
    try:
        with open(os.path.dirname(os.path.realpath(__file__)) + "\\cave_projector_conf.json") as file:
            configData = json.load(file)
            if "com_ports" in configData.keys() and "projector_commands" in configData.keys():
                return configData
            else:
                raise ValueError
    except OSError as err:
        print("OS error:", err)
    except ValueError:
        print("Value error:\nProjector conf file seems to be malformed or corrupted.")
    except Exception as err:
        print(f"Unexpected {err=}, {type(err)=}")
        raise


def validate_command(commandinput: str):
    for command in Commands:
        if commandinput.upper().strip() == command.value:
            return command
    return Commands.ERROR


def request_user_input() -> Commands:
    userInput = input("Please enter a projector command (Supported commands: ON, OFF):\n")
    return validate_command(userInput)


def send_to_port(port: str, command: bytes, baudrate: int, parity: str, stopbits: int, bytesize: int) -> None:
    serialPort = serial.Serial(
        port=port,
        baudrate=baudrate,
        parity=parity,
        stopbits=stopbits,
        bytesize=bytesize
    )

    if serialPort.isOpen():
        serialPort.close()
    serialPort.open()
    serialPort.write(command)
    time.sleep(0.3)
    serialPort.flush()
    serialPort.close()


def execute_command(configdata: dict, givencommand: Commands):
    for port in configdata["com_ports"].keys():
        # print(port)
        projectorName = configdata["com_ports"][port]
        # print(projectorName)
        try:
            portCommands = configdata["projector_commands"][configdata["com_ports"][port]]
            # print(portCommands)
            try:
                commandString = portCommands[givencommand.value]
                try:
                    send_to_port(port, commandString.encode(), 9600, serial.PARITY_NONE,
                                 serial.STOPBITS_ONE, serial.EIGHTBITS)
                except Exception as err:
                    print(f"Unexpected {err=}, {type(err)=}")
                    raise
            except KeyError as err:
                print(f"Unexpected Key Error: Command \"{err.args[0]}\" was not found in {projectorName} commands.")
                raise
        except KeyError as err:
            print(f"Unexpected Key Error: Projector type {err.args[0]} is not specified in projector_commands.")
            raise


if __name__ == '__main__':
    # print(sys.argv)
    requestedcommand = Commands.ERROR
    if len(sys.argv) == 1:
        requestedcommand = request_user_input()
    elif len(sys.argv) == 2:
        requestedcommand = validate_command(sys.argv[1])
    config_data = read_config()
    execute_command(config_data, requestedcommand)
