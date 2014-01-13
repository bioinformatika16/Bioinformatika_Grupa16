class InputData:
    def __init__(self, file_path):
        try:
            input_file = open(file_path, 'r')
        except IOError:
            print ("ERROR: Cant open file: " + file_path)

        print ("INFO: processing input file.")
        raw_sequences = input_file.readlines()
        self.sequences = list()
        for raw_sequence in raw_sequences:
            clean_sequence = list()
            for char in str.strip(raw_sequence):
                upper_char = str(char).upper()
                if upper_char == 'A':
                    clean_sequence.append(upper_char)
                elif upper_char == 'C':
                    clean_sequence.append(upper_char)
                elif upper_char == 'G':
                    clean_sequence.append(upper_char)
                elif upper_char == 'T':
                    clean_sequence.append(upper_char)
                elif upper_char == '-':
                    clean_sequence.append(upper_char)
                else:
                    raise ValueError
            self.sequences.append(clean_sequence)
        input_file.close()
        print ("INFO: input file processed.")
        return


class OutputData:
    def __init__(self, file_path, outputs):
        try:
            output_file = open(file_path, 'w')
        except IOError:
            print("ERROR: Cant open/create file: " + file_path)
            output_file = None

        print("INFO: Writing output to: {0}".format(output_file.name))
        output_strings = list()
        for output in outputs:
            output_strings.append(str().join(output))

        for string in output_strings:
            output_file.write(string + '\n')

        output_file.close()
        return
